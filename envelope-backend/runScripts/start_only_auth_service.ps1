$composeDirectory = "../docker-compose.yml"

$process = Start-Process -FilePath "docker" -ArgumentList "compose up auth_service auth_service_db rabbit_mq" -NoNewWindow -PassThru

$handler = {
    & docker compose down --volumes --remove-orphans
	
    Stop-Process -Id $process.Id -Force
}

Register-EngineEvent PowerShell.Exiting -Action $handler

Wait-Process -Id $process.Id

Unregister-Event -SourceIdentifier PowerShell.Exiting
