-- Table: public.Order

-- DROP TABLE IF EXISTS public."Order";

CREATE TABLE IF NOT EXISTS public."Order"
(
    "Id" SERIAL integer NOT NULL,
    "CreateDate" date,
    "UpdatedDate" date,
    "ProductId" integer,
    "Status" integer,
    CONSTRAINT "Order_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Product_Order" FOREIGN KEY ("ProductId")
        REFERENCES public."Product" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Order"
    OWNER to newuser;