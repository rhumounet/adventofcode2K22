#!/bin/sh

namespace=$1
className=$2

cat > "../AdventOfCode2K22.Test/Le travail/$namespace/$className.Test.cs" << EOF
using $namespace;
using NUnit.Framework;

namespace Test.$namespace;

[TestFixture]
public class ${className}Test : SolverBaseTest<$className>
{
    public ${className}Test() : base(new ${className}(), "$namespace/input.txt")
    {
    }
}
EOF