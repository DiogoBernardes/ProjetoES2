-- noinspection SqlResolveForFile

-- Insert initial data into the role table     
INSERT INTO roles (name)
VALUES ('Admin'),
       ('UserManager'),
       ('User');

-- Insert initial data into the users table                                                                                                       
INSERT INTO users (name, username, password,email,phone,role_id)
VALUES ('Diogo Bernardes', 
        'Bernardes_', 
        '123',
        'DiogoBernardes@ipvc.pt',
        '935471697',
        (SELECT id FROM roles WHERE name = 'Admin' LIMIT 1));

-- Insert initial data into the ticket type table
INSERT INTO ticket_type(name) 
VALUES('Normal'),
      ('VIP'),
      ('Backstage'),
      ('Familiar'),
      ('Estudante');


-- Insert initial data into regist state table
INSERT INTO regist_state(state)
VALUES ('Confirmado'),
       ('Cancelado');