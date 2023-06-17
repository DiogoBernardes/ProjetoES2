--Role table: This table will have the possible roles for a user
CREATE TABLE roles (
        id                  uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
        name                VARCHAR(255) NOT NULL
);
--Users table: This table have the information of a user 
CREATE TABLE users (
        id                  uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
        name                VARCHAR(100) NOT NULL,
        username            VARCHAR(100) NOT NULL,
        password            VARCHAR(100) NOT NULL,
        email               VARCHAR(100) NOT NULL,
        phone               VARCHAR(9) NOT NULL,
        role_id             uuid NOT NULL REFERENCES roles(id)
);
CREATE TABLE event_category(
        id                  uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
        name                VARCHAR(100) NOT NULL
);
--Event table: This table have the information of a event
CREATE TABLE event_info (
        id                  uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
        organizer_id        uuid NOT NULL REFERENCES users(id),
        name                VARCHAR(100) NOT NULL,
        date_hour           TIMESTAMP WITH TIME ZONE NOT NULL,
        localization        VARCHAR(100) NOT NULL,
        description         VARCHAR(100) NOT NULL,
        capacity            INT NOT NULL,
        category            uuid NOT NULL REFERENCES event_category(id)
);

--Message table: This table have the information of a message
CREATE TABLE messages (
        id                  uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
        participant_id      uuid NOT NULL REFERENCES users(id),
        organizer_id        uuid NOT NULL REFERENCES users(id),
        event_id            uuid NOT NULL REFERENCES event_info(id),
        message_content     VARCHAR(500) NOT NULL
);

--Activity table: This table have the information of a activity
CREATE TABLE activity_info (
        id                  uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
        name                VARCHAR(100) NOT NULL,
        description         VARCHAR(100) NOT NULL,
        event_id            uuid NOT NULL REFERENCES event_info(id)
);
--Activity_participant table: This table have the information of the participants of a activity
CREATE TABLE activity_participant(
        id                  uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
        participant_id      uuid NOT NULL REFERENCES users(id),
        activity_id         uuid NOT NULL REFERENCES activity_info(id)
);
--Ticket_type table: This table have the information of the ticket types
CREATE TABLE ticket_type(
       id                   uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
       name                 VARCHAR(50) NOT NULL 
);

--Event_Ticket Table: This table contains the quantity and price information of a certain type of tickets for a specific event
CREATE TABLE event_ticket (
        id                  uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
        ticket_type         uuid NOT NULL REFERENCES ticket_type(id),
        event_id            uuid NOT NULL REFERENCES event_info(id),
        quantity            INT NOT NULL,
        price               NUMERIC(10,2) NOT NULL         
);
--Regist_state Table: This table contains the types of states that a regist on event can be 
CREATE TABLE regist_state(
        id                  uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
        state               VARCHAR(50) NOT NULL 
);

--event_regist Table: This table contains the information of a regist on a event 
CREATE TABLE event_regist ( 
        id                  uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
        event_id            uuid NOT NULL REFERENCES event_info(id),
        participant_id      uuid NOT NULL REFERENCES users(id),
        regist_date         TIMESTAMP WITH TIME ZONE NOT NULL,
        state_id            uuid NOT NULL REFERENCES regist_state(id),
        ticket_type_id      uuid NOT NULL REFERENCES ticket_type(id)
);