CREATE TABLE TaskEvents(
    id UUID NOT NULL,
    versionId INT NOT NULL,
    date TIMESTAMP NOT NULL,
    eventData JSON NOT NULL,
    PRIMARY KEY (id, versionId)
);