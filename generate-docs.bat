@echo off
rem Crear el directorio principal
mkdir docs

rem Crear los archivos vacíos dentro de /docs
cd docs
type null > 01-PRD.md
type null > 02-ARCHITECTURE.md
type null > 03-BACKLOG.md
type null > 04-DOMAIN-MODEL.md
type null > 05-USER-STORIES.md
type null > 06-ACCEPTANCE-CRITERIA.md
type null > 07-TRACEABILITY-MATRIX.md
type null > 08-API-CONTRACT.md
type null > 09-TEST-PLAN.md
type null > 10-PROMPTS.md
cd ..

echo Estructura /docs creada con exito.
pause