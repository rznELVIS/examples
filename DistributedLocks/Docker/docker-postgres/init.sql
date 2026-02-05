CREATE TABLE IF NOT EXISTS counter (
    id SERIAL PRIMARY KEY,
    value integer NOT NULL
);

CREATE TABLE IF NOT EXISTS log (
    id SERIAL PRIMARY KEY,
    message varchar(2000) NULL,
    logged_at TIMESTAMPTZ NOT NULL
);

INSERT INTO counter (value) VALUES
    (0);
