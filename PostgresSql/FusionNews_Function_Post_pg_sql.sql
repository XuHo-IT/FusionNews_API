-- Get All Post function --
CREATE OR REPLACE FUNCTION public.usf_get_all_post(
	)
    RETURNS TABLE(postid integer, title character varying, content character varying, newsofpostid integer, createdon timestamp with time zone) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY
    SELECT 
        post.post_id::int,
        post.title::VARCHAR,
        post.content::VARCHAR,
        post.news_of_post_id::int,
        post.created_on::TIMESTAMP with time zone
    FROM post;
END;
$BODY$;

-- Create Post function
CREATE OR REPLACE FUNCTION public.usf_create_post(
	json_input jsonb)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    title TEXT;
    content TEXT;
	news_of_post_id INTEGER;
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
$BODY$;