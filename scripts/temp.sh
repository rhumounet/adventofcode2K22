#!/bin/sh

for i in {3..25}
do
    ./newday.sh Day$i Part1
    ./newday.sh Day$i Part2
done