CREATE OR REPLACE FUNCTION usp_create_post(json_input JSONB)
RETURNS VOID AS $$
DECLARE
    title TEXT;
    content TEXT;
BEGIN
    -- Trích xuất các giá trị từ JSON
    title := json_input->>'Title';
    content := json_input->>'Content';

    -- Kiểm tra nếu dữ liệu hợp lệ
    IF title IS NULL OR content IS NULL THEN
        RAISE EXCEPTION 'Invalid input: Title and Content are required';
    END IF;

    -- Chèn vào bảng Post
    INSERT INTO post (title, content, created_on)
    VALUES (title, content, NOW());
END;
$$ LANGUAGE plpgsql;