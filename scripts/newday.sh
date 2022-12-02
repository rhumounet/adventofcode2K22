#!/bin/sh

namespace=$1
className=$2

./newsolve.sh $namespace $className
./newtest.sh $namespace $className