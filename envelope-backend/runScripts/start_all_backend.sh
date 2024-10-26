#!/bin/bash

COMPOSE_FILE="../docker-compose.yml"

if ! command -v docker &> /dev/null; then
    echo "Docker не установлен. Пожалуйста, установите Docker."
    exit 1
fi

if ! command -v docker-compose &> /dev/null; then
    echo "Docker Compose не установлен. Пожалуйста, установите Docker Compose."
    exit 1
fi

docker-compose -f "$COMPOSE_FILE" up -d

docker-compose -f "$COMPOSE_FILE" ps

echo "Нажмите [CTRL+C] для остановки и удаления контейнеров..."
trap 'docker-compose -f "$COMPOSE_FILE" down --rmi all; exit' SIGINT

while :; do sleep 1; done