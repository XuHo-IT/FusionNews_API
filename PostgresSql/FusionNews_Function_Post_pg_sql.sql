-- Get All Post function --
CREATE OR REPLACE FUNCTION usf_get_all_post()
RETURNS TABLE(
	postid int, 
	title varchar(100), 
	content text, 
	newsofpostid int, 
	createdon timestamp with time zone
	)AS $$
BEGIN
    RETURN QUERY
    SELECT 
        post.post_id,
        post.title::VARCHAR,
        post.content,
        post.news_of_post_id,
        post.created_on
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
	createdon timestamp with time zone
)AS $$
DECLARE
    id INT;
BEGIN
    -- Extract the 'id' value from the JSON input
    id := (json_input->>'id')::INT;
 
    -- Query the 'post' table based on the extracted id
    RETURN QUERY
    SELECT 
        post.post_id::INTEGER,
        post.title::VARCHAR,
        post.content::TEXT, -- Corrected to match TEXT type
        post.news_of_post_id::INTEGER,
        post.created_on::TIMESTAMP WITH TIME ZONE
    FROM post
    WHERE post.post_id = id;
END;
$$ LANGUAGE plpgsql;

-- Create Post function
CREATE OR REPLACE FUNCTION usf_create_post(json_input jsonb)
RETURNS void
AS $$
DECLARE
    title varchar(100);
    content TEXT ;
	newsof_post_id int;
BEGIN
    -- Trích xuất các giá trị từ JSON
    title := json_input->>'Title';
    content := json_input->>'Content';
	news_of_post_id := json_input->>'NewsOfPostId';

    -- Kiểm tra nếu dữ liệu hợp lệ
    IF news_of_post_id IS NULL THEN
        INSERT INTO post (title, content, created_on)
        VALUES (title, content, NOW());
    ELSE
        INSERT INTO post (title, content, news_of_post_id, created_on)
        VALUES (title, content, news_of_post_id, NOW());
    END IF;

END;
$$ LANGUAGE plpgsql;

-- Update Post
CREATE OR REPLACE FUNCTION usf_update_post(json_input jsonb)
returns void
AS $$
DECLARE
    post_id int,
    title varchar(100);
    content TEXT ;
BEGIN
    -- Trích xuất các giá trị từ JSON
    post_id := (json_input->>'PostId')::INT;
    title := json_input->>'Title';
    content := json_input->>'Content';
    UPDATE post
        SET 
            title = COALESCE(title, title), -- Chỉ cập nhật nếu không NULL
            content = COALESCE(content, content),
        WHERE post_id = post_id;
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
    WHERE post_id 
END;
$$ LANGUAGE plpgsql;