# Ce script permet d'appeler la foncion HTTP Trigger de l'API Azure

curl --data-binary "@cat.jpg" -X POST "https://thomas-soupizet-lisa-lupi-fa.azurewebsites.net/api/HttpTrigger?w=100&h=0" -v > output.jpeg