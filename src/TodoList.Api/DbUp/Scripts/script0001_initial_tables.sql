create table if not exists `users`
(
    `id`   int unsigned auto_increment primary key,
    `name` varchar(191) not null
) charset = utf8mb4;
