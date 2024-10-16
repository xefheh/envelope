CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE en_role (
    id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    name varchar(255) NOT NULL
);

CREATE TABLE en_user (
    id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    nickname varchar(255) NOT NULL,
    role_id UUID NOT NULL,
    email varchar(255) NOT NULL,
    password TEXT NOT NULL,
    FOREIGN KEY (role_id) REFERENCES en_role(id)
);

INSERT INTO public.en_role(name) VALUES ('Student');