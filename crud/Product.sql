-- Table: public.Product

-- DROP TABLE IF EXISTS public."Product";

CREATE TABLE IF NOT EXISTS public."Product"
(
    "Id" SERIAL integer NOT NULL,
    "Name" character(10) COLLATE pg_catalog."default" NOT NULL,
    "Description" character(30) COLLATE pg_catalog."default",
    "Weight" double precision NOT NULL,
    "Height" double precision NOT NULL,
    "Width" double precision NOT NULL,
    "Length" double precision NOT NULL,
    CONSTRAINT "Product_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Product"
    OWNER to newuser;