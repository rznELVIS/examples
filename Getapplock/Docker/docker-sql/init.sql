-- Проверка и создание таблицы counter
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'counter')
BEGIN
    CREATE TABLE counter (
        id INT IDENTITY(1,1) PRIMARY KEY,
        value INT NOT NULL
    );
END;

-- Проверка и создание таблицы log
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'log')
BEGIN
    CREATE TABLE log (
        id INT IDENTITY(1,1) PRIMARY KEY,
        message VARCHAR(2000) NULL,
        logged_at DATETIMEOFFSET NOT NULL
    );
END;

-- Проверка и создание таблицы lock
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'lock')
BEGIN
    CREATE TABLE [lock] (
        resource VARCHAR(100) PRIMARY KEY,
        locked_by VARCHAR(100),
        locked_at DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
        expires_at DATETIMEOFFSET
    );
END;

-- Вставка начального значения в counter (если таблица пуста)
IF NOT EXISTS (SELECT 1 FROM counter)
BEGIN
    INSERT INTO counter (value) VALUES (0);
END;