# API Readme

This readme is consist of notes of failure only.

## Notes

This notes is written in Bahasa Indonesia.

### Connection string at API

[Link](https://github.com/johaneka06/Werewolf.NET/blob/master/API/Controllers/PlayerController.cs#L28)

connection string jangan taro di API layer. itu harusnya infrastructure layer.

paling bagusnya bikin .env gitu, tapi karena ini toy project aja, bikin hard code string aja kyk gini tapi di infrastructure layer (di implementasi repo).

### Open connection at API

[Link](https://github.com/johaneka06/Werewolf.NET/blob/master/API/Controllers/PlayerController.cs#L29)

open connection juga jgn sampe muncul di API layer.

pokoknya dibuat sedemikian rupa supaya di Controller gak ada ```using Npgsql```. kalau using ```Werewolf.NET.Game.Database.PostgreSQL``` boleh.

biar bagus, Controller punya kontrol terhadap transaksi db juga. jadi dia bisa nentuin kapan rollback kapan commit. misal pake unit of work atau pake dbcontext.

terus controller2 yang lain emang baru testing ya? flow-nya masih tembak gitu



*Notes: API masih testing. Flow masih tembak - tembak dulu"*