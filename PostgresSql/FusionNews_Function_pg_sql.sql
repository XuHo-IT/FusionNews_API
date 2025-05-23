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

-- SELECT usf_create_question('{
--   "Question": "What is the weather today?",
--   "Answer": "Cloudy day."
-- }'::jsonb);

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
RETURNS TABLE (
    question_id INT,
    answer TEXT
)
AS $$
DECLARE
    qid INT;
BEGIN
    -- Extract values from JSON input
    qid := (json_input->>'questionid')::INT;

    -- Return both question_id and answer
    RETURN QUERY
    SELECT 
        cq.question_id,
        cq.answer
    FROM public.chatbot_question cq
    WHERE cq.question_id = qid;
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
-- Get All Post function --
CREATE OR REPLACE FUNCTION usf_get_all_post()
RETURNS TABLE(
	postid int, 
	title varchar(100), 
	content text, 
	newsofpostid int, 
	createat TIMESTAMPTZ, -- timestamp with time zone = TIMESTAMPTZ
    updateat TIMESTAMPTZ,
	userid text
	)AS $$
BEGIN
    RETURN QUERY
    SELECT 
        post.post_id,
        post.title::VARCHAR,
        post.content,
        post.news_of_post_id,
        post.create_at,
        post.update_at,
		post.user_id
    FROM post;
END;
$$ LANGUAGE plpgsql;

-- Find Post by Id
CREATE OR REPLACE FUNCTION usf_find_post_by_id(json_input jsonb)
RETURNS TABLE(
    postid int, 
    title varchar(100), 
    content text, 
    newsofpostid int, 
    createat TIMESTAMPTZ,
    updateat TIMESTAMPTZ,
	userid text,
    comments json
)AS $$
DECLARE
    id INT;
BEGIN
    -- Extract the 'id' value from the JSON input
    id := (json_input->>'id')::INT;
 
    -- Query the 'post' table based on the extracted id
    RETURN QUERY
    SELECT 
        p.post_id,
        p.title::VARCHAR,
        p.content,
        p.news_of_post_id,
        p.create_at,
        p.update_at,
		p.user_id,
        COALESCE(
            JSON_AGG(
                JSON_BUILD_OBJECT(
                    'commentid', c.comment_id,
                    'content', c.content,
                    'createat', c.create_at,
                    'updateat', c.update_at,
					'userid', c.user_id
                )
            ) FILTER (WHERE c.comment_id IS NOT NULL),
            '[]'::json
        ) AS comments
    FROM post p
    LEFT JOIN comment c ON p.post_id = c.post_id
    WHERE p.post_id = id
    GROUP BY 
        p.post_id, p.title, p.content, p.news_of_post_id, p.create_at, p.update_at, p.user_id;
END;
$$ LANGUAGE plpgsql;

-- Create Post function
CREATE OR REPLACE FUNCTION usf_create_post(json_input jsonb)
RETURNS void
AS $$
DECLARE
    title varchar(100);
    content TEXT ;
	news_of_post_id int;
	user_id text;
BEGIN
    -- Extract values from JSON
    title := json_input->>'Title';
    content := json_input->>'Content';
	news_of_post_id := json_input->>'NewsOfPostId';
	user_id := json_input->>'UserId';

    -- Kiểm tra nếu dữ liệu hợp lệ
    IF news_of_post_id IS NULL THEN
        INSERT INTO post (title, content, create_at, user_id)
        VALUES (title, content, NOW(), user_id);
    ELSE
        INSERT INTO post (title, content, news_of_post_id, create_at, user_id)
        VALUES (title, content, news_of_post_id, NOW(), user_id);
    END IF;

END;
$$ LANGUAGE plpgsql;

-- Update post
CREATE OR REPLACE FUNCTION usf_update_post(json_input jsonb)
RETURNS TABLE (postid int, title text, content text, create_at TIMESTAMPTZ, update_at TIMESTAMPTZ)
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
        content = COALESCE(u_content, p.content),
        update_at = NOW()
    WHERE p.post_id = u_post_id;
END;
$$ LANGUAGE plpgsql;

-- Delete Post
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
------------------------------------------- Comments Function -------------------------------------------  
-- Get All Comment function --
CREATE OR REPLACE FUNCTION usf_get_all_comments(json_input jsonb)
RETURNS TABLE(
	commentid int, 
	content text, 
	createat TIMESTAMPTZ, -- timestamp with time zone = TIMESTAMPTZ
    updateat TIMESTAMPTZ,
    postid int,
	userid TEXT
	)AS $$
DECLARE
    g_post_id int;
BEGIN
    -- Extract the 'id' value from the JSON input
    g_post_id := (json_input->>'postId')::INT;

    RETURN QUERY
    SELECT 
        c.comment_id,
        c.content,
        c.create_at,
        c.update_at,
        c.post_id,
		c.user_id
    FROM comment c
    WHERE c.post_id = g_post_id;
END;
$$ LANGUAGE plpgsql;

-- Find Comment by Id
CREATE OR REPLACE FUNCTION usf_find_comment_by_id(json_input jsonb)
RETURNS TABLE(
    commentid int, 
    content text, 
    createat TIMESTAMPTZ,
    updateat TIMESTAMPTZ,
    postid int,
	userid TEXT
    ) AS $$
DECLARE
    f_id int;
BEGIN
    -- Extract the 'id' value from the JSON input
    f_id := (json_input->>'id')::INT;
    RETURN QUERY
    SELECT 
        c.comment_id,
        c.content,
        c.create_at,
        c.update_at,
        c.post_id,
		c.user_id
    FROM comment c
    WHERE c.comment_id = f_id;
END;
$$ LANGUAGE plpgsql;

-- Create Comment function
CREATE OR REPLACE FUNCTION usf_create_comment(json_input jsonb)
RETURNS void
AS $$
DECLARE
	c_user_id varchar;
    c_content TEXT;
    c_post_id int;
BEGIN
    -- Extract values from JSON
	c_user_id := json_input->>'UserId';
    c_content := json_input->>'Content';
    c_post_id := json_input->>'PostId';

    -- Insert into table
    INSERT INTO comment (user_id, content, post_id, create_at)
    VALUES (c_user_id, c_content, c_post_id, NOW());
END;
$$ LANGUAGE plpgsql;

-- Update Comment
CREATE OR REPLACE FUNCTION usf_update_comment(json_input jsonb)
RETURNS TABLE (commentid int, content text, createat TIMESTAMPTZ, updateat TIMESTAMPTZ, postid int)
AS $$
DECLARE
    u_comment_id int;
    u_content TEXT;
BEGIN
    -- Extract values from JSON
    u_comment_id := (json_input->>'CommentId')::INT;
    u_content := json_input->>'Content';
    -- Update with alias to avoid ambiguity
    UPDATE comment c
    SET 
        content = COALESCE(u_content, c.content),
        update_at = NOW()
    WHERE c.comment_id = u_comment_id;
    -- Return the updated row
    RETURN QUERY
    SELECT 
        c.comment_id,
        c.content,
        c.create_at,
        c.update_at,
        c.post_id
    FROM comment c
    WHERE c.comment_id = u_comment_id;
END;
$$ LANGUAGE plpgsql;

-- Delete Comment
CREATE OR REPLACE FUNCTION usf_delete_comment(json_input jsonb)
RETURNS void
AS $$
DECLARE
    id INT;
BEGIN
    -- Extract the 'id' value from the JSON input
    id := (json_input->>'id')::INT;
 
    DELETE FROM comment
    WHERE comment_id = id;
END;
$$ LANGUAGE plpgsql;