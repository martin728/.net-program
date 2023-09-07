CREATE OR REPLACE PROCEDURE GetFilteredOrders(
    IN filter_year INT,
    IN filter_month INT,
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
        (filter_year IS NULL OR EXTRACT(YEAR FROM "CreateDate") = filter_year)
        AND (filter_month IS NULL OR EXTRACT(MONTH FROM "CreateDate") = filter_month)
        AND (filter_status IS NULL OR "Status"::TEXT = filter_status)
        AND (filter_product_id IS NULL OR "ProductId" = filter_product_id);
END;
$$;
