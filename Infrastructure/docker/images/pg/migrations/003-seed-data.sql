-- noinspection SqlResolveForFile

-- Insert initial data into the role table     
INSERT INTO roles (name)
VALUES ('Admin'),
       ('UserManager'),
       ('User');

-- Insert initial data into the users table                                                                                                       
INSERT INTO users (name, username, password,email,role_id)
VALUES ('Diogo Bernardes', 
        'Bernardes_', 
        '123',
        'DiogoBernardes@ipvc.pt',
        (SELECT id FROM roles WHERE name = 'Admin' LIMIT 1));
