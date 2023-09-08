CREATE OR REPLACE PROCEDURE GetFilteredOrders(
    IN create_date DATE,
    IN updated_date DATE,
    IN filter_status TEXT,
    IN filter_product_id INT,
    OUT result_set refcursor
)
LANGUAGE plpgsql
AS $$
BEGIN
    OPEN result_set FOR
    SELECT "Id", "CreateDate", "UpdatedDate", "ProductId", "Status"
    FROM public."Order"
    WHERE
        (create_date IS NULL OR "CreateDate" = create_date)
        AND (updated_date IS NULL OR "UpdatedDate" = updated_date)
        AND (filter_status IS NULL OR "Status"::TEXT = filter_status)
        AND (filter_product_id IS NULL OR "ProductId" = filter_product_id);
END;
$$;
