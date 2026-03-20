CREATE TABLE IF NOT EXISTS counter (
    id SERIAL PRIMARY KEY,
    value integer NOT NULL
);

CREATE TABLE IF NOT EXISTS log (
    id SERIAL PRIMARY KEY,
    message varchar(2000) NULL,
    logged_at TIMESTAMPTZ NOT NULL
);

CREATE TABLE IF NOT EXISTS lock (
    resource VARCHAR(100) PRIMARY KEY,
    locked_by VARCHAR(100),
    locked_at TIMESTAMPTZ DEFAULT NOW(),
    expires_at TIMESTAMPTZ
);

INSERT INTO counter (value) VALUES
    (0);
