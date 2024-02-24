FROM SqlServer: 16.0.10
EXPOSE 3306
COPY ./RestWithAspNET.API/db/migrations/ /home/database/
COPY ./RestWithAspNET.API/db/dataset/ /home/database/
COPY ./RestWithAspNET.API/ci/init_database.sh/ /docker.entrypoint-initdb.d/init_database.sh