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



-- DROP FUNCTION IF EXISTS usf_get_all_questions();
-- SELECT * FROM usf_get_all_questions();



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

-- ALTER TABLE chatbot_question  /* for drop col have not nullable*/
-- ALTER COLUMN update_at DROP NOT NULL;

SELECT usf_create_question('{
  "Question": "What is the weather today?",
  "Answer": "Cloudy day."
}'::jsonb);

-- TRUNCATE TABLE chatbot_question RESTART IDENTITY; /* Restart at 1 ( deleted all rows)*/

--  /* deleted rows and want the next ID to follow the current highest ID*/
-- SELECT setval('chatbot_question_id_seq', COALESCE((SELECT MAX(id) FROM chatbot_question), 1), true);

-- Update question
CREATE OR REPLACE FUNCTION usf_update_question(json_input jsonb)
RETURNS TABLE (
    questionid INT,
    question TEXT,
    answer TEXT,
    createat TIMESTAMPTZ,
    updateat TIMESTAMPTZ
)
AS $$
DECLARE
    u_question_id INT;
    u_question TEXT;
    u_answer TEXT;
BEGIN
    -- Extract fields from JSON input
    u_question_id := (json_input->>'QuestionId')::INT;
    u_question := json_input->>'Question';
    u_answer := json_input->>'Answer';

    -- Perform the update
    UPDATE public.chatbot_question cq
    SET
        question = COALESCE(u_question, cq.question),
        answer = COALESCE(u_answer, cq.answer),
        update_at = NOW()
    WHERE cq.question_id = u_question_id;

    -- Return the updated row
    RETURN QUERY
    SELECT 
        cq.question_id,
        cq.question,
        cq.answer,
        cq.create_at,
        cq.update_at
    FROM public.chatbot_question cq
    WHERE cq.question_id = u_question_id;
END;
$$ LANGUAGE plpgsql;


-- Delete question
CREATE OR REPLACE FUNCTION usf_delete_question(json_input jsonb)
RETURNS void
AS $$
DECLARE
    questionid INT;
BEGIN
    -- Extract 'id' from JSON input
    questionid := (json_input->>'questionid')::INT;

    DELETE FROM public.chatbot_question
    WHERE question_id = questionid;
END;
$$ LANGUAGE plpgsql;

-- Delete question with ID 3
SELECT usf_delete_question('{"questionid": 3}'::jsonb);

-- Update a question
SELECT * FROM usf_update_question('{
    "QuestionId": 1,
    "Question": "What is PostgreSQL?",
    "Answer": "PostgreSQL is an open-source relational database and good for using with C#."
}'::jsonb);

CREATE OR REPLACE FUNCTION usf_get_questions()
RETURNS TABLE (
    question_id INT,
    question TEXT
)
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        cq.question_id,
        cq.question
    FROM public.chatbot_question cq;
END;
$$ LANGUAGE plpgsql;


DROP FUNCTION usf_get_questions()
SELECT * FROM usf_get_questions();


CREATE OR REPLACE FUNCTION usf_get_answer(json_input jsonb)
RETURNS TEXT
AS $$
DECLARE
    qid INT;
    result TEXT;
BEGIN
    -- Extract values from JSON input
    qid := (json_input->>'questionid')::INT;

    -- Look for the question by ID and text
    SELECT answer INTO result
    FROM public.chatbot_question
    WHERE question_id = qid ;

    RETURN result;
END;
$$ LANGUAGE plpgsql;


	SELECT usf_get_answer('{
    "questionid": 1
}'::jsonb);

------------------------------------------- User Function -------------------------------------------  
-- DROP FUNCTION IF EXISTS usf_get_user_by_username(text)

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


------------------------------------------- Post Function -------------------------------------------  
-- Create post
CREATE OR REPLACE FUNCTION usf_create_post(json_input JSONB)
RETURNS VOID AS $$ 
DECLARE
    title TEXT;
    content TEXT;
BEGIN
    -- Extract values from JSON
    title := json_input->>'Title';
    content := json_input->>'Content';

    -- Check if data is valid
    IF title IS NULL OR content IS NULL THEN
        RAISE EXCEPTION 'Invalid input: Title and Content are required';
    END IF;

    -- Insert into Post table
    INSERT INTO post (title, content, created_on)
    VALUES (title, content, NOW());
END;
$$ LANGUAGE plpgsql;

-- Update post
CREATE OR REPLACE FUNCTION usf_update_post(json_input jsonb)
RETURNS TABLE (postid int, title text, content text)
AS $$
DECLARE
    u_post_id int;
    u_title varchar(100);
    u_content TEXT;
BEGIN
    -- Trích xuất các giá trị từ JSON
    u_post_id := (json_input->>'PostId')::INT;
    u_title := json_input->>'Title';
    u_content := json_input->>'Content';
 
    -- Cập nhật với alias để tránh mập mờ
    UPDATE post p
    SET
        title = COALESCE(u_title, p.title),
        content = COALESCE(u_content, p.content)
    WHERE p.post_id = u_post_id;
 
    -- Trả về bản ghi sau khi cập nhật
    RETURN QUERY
    SELECT p.post_id, p.title, p.content
    FROM post p
    WHERE p.post_id = u_post_id;
END;
$$ LANGUAGE plpgsql;

-- Delete post
CREATE OR REPLACE FUNCTION usf_delete_post(json_input jsonb)
RETURNS void
AS $$
DECLARE
    id INT;
BEGIN
    -- Extract the 'id' value from the JSON input
    id := (json_input->>'id')::INT;
 
    DELETE FROM post
    WHERE post_id = id;
END;
$$ LANGUAGE plpgsql;
SELECT usf_delete_post('{"id": 3}'::jsonb);

