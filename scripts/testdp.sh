#!/bin/sh

day=$1
part=$2

dotnet test "./AdventOfCode2K22.Test/" --filter Day$day.Part$part