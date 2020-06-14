--Database: Postgresql 12

--CREATE TABLE "user"
(
	userId 		UUID PRIMARY KEY,
	userName	VARCHAR(255),
	created_at	TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	updated_at	TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE "role"
(
	roleId		SERIAL PRIMARY KEY,
	roleName	VARCHAR(255),
	created_at	TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	updated_at	TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE "exp"
(
	expId		UUID PRIMARY KEY,
	userId		UUID,
	xpValue		INT,
	created_at	TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (userId) REFERENCES "user"(userId)
);

CREATE TABLE "game_room"
(
	roomId		UUID PRIMARY KEY,
	maxPlayer	INT,
	created_at	TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	updated_at	TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	deleted_at	TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE "playing_room"
(
	roomId		UUID,
	userId		UUID,
	roleId		INT,
	created_at	TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	deleted_at	TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (roomId) REFERENCES "game_room"(roomId),
	FOREIGN KEY (userId) REFERENCES "user"(userId),
	FOREIGN KEY (roleId) REFERENCES "role"(roleId)
);
