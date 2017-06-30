#!/bin/bash

cd ../RunUO
nohup mono RunUO.exe > Logs/output.log &
export RUNUO_PID=$!
cd ../Scripts
