------------------------------------------- Questions Function -------------------------------------------  

CREATE OR REPLACE FUNCTION usf_get_all_questions()
RETURNS TABLE (
    questionid INT,
    question TEXT,
    answer TEXT,
    createat TIMESTAMPTZ,
    updateat TIMESTAMPTZ
)
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        q.question_id,
        q.question,
        q.answer,
        q.create_at,
        q.update_at
    FROM public.chatbot_question q;
END;
$$ LANGUAGE plpgsql;



DROP FUNCTION IF EXISTS usf_get_all_questions();
SELECT * FROM usf_get_all_questions();



CREATE OR REPLACE FUNCTION usf_create_question(json_input jsonb)
RETURNS void
AS $$
DECLARE
    question TEXT;
    answer TEXT;
BEGIN
    question := json_input->>'Question';
    answer := json_input->>'Answer';

    -- Check for null or empty values and throw error
    IF question IS NULL OR answer IS NULL OR question = '' OR answer = '' THEN
        RAISE EXCEPTION 'Question and Answer cannot be null or empty';
    END IF;

    -- Insert into table
    INSERT INTO chatbot_question (question, answer, create_at, update_at)
    VALUES (question, answer, NOW(),NULL);
END;
$$ LANGUAGE plpgsql;

ALTER TABLE chatbot_question  /* for drop col have not nullable*/
ALTER COLUMN update_at DROP NOT NULL;

SELECT usf_create_question('{
  "Question": "What is PostgreSQL?",
  "Answer": "PostgreSQL is an open-source relational database."
}'::jsonb);

TRUNCATE TABLE chatbot_question RESTART IDENTITY; /* Restart at 1 ( deleted all rows)*/

 /* deleted rows and want the next ID to follow the current highest ID*/
SELECT setval('chatbot_question_id_seq', COALESCE((SELECT MAX(id) FROM chatbot_question), 1), true);


------------------------------------------- User Function -------------------------------------------  
DROP FUNCTION IF EXISTS usf_get_user_by_username(text)

CREATE OR REPLACE FUNCTION usf_get_user_by_username(p_username TEXT)
RETURNS TABLE (
    Id INT,
    Username TEXT,
    PasswordHash TEXT,
    Email TEXT
)
AS $$
BEGIN
    RETURN QUERY
    SELECT u.id, u.username, u.password_hash, u.email
    FROM users u
    WHERE u.username = p_username;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION usf_is_username_taken(p_username TEXT)
RETURNS BOOLEAN
AS $$
DECLARE
    exists BOOLEAN;
BEGIN
    SELECT EXISTS (
        SELECT 1 FROM users WHERE username = p_username
    ) INTO exists;

    RETURN exists;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION usf_add_user(j_input JSONB)
RETURNS VOID
AS $$
BEGIN
    INSERT INTO users (username, password_hash, email)
    VALUES (
        j_input->>'Username',
        j_input->>'PasswordHash',
        j_input->>'Email'
    );
END;
$$ LANGUAGE plpgsql;

