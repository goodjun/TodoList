create table if not exists `todos`
(
    `id`   int unsigned auto_increment primary key,
    `title` varchar(256) not null,
    `content` varchar(1024) not null,
    `is_done` tinyint(1) unsigned not null
) charset = utf8mb4;
