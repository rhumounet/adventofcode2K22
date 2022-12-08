#!/bin/sh

day=$1

dotnet test "./AdventOfCode2K22.Test/" --filter Day$day