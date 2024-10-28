# envelope
-для установки зависимостей pip install -r requirements.txt
-для создания миграции alembic revision --autogenerate -m "initial"
-применить миграцию alembic upgrade head
-запуск приложения uvicorn main:app --reload из папки app
-после запуска в браузере поменяйте в строке на http://127.0.0.1:8000/docs для сваггера