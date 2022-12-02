#!/bin/sh

namespace=$1
className=$2

cat > "../AdventOfCode2K22/Le travail/$namespace/$className.cs" << EOF
namespace $namespace;

public class $className : AbstractSolver 
{
    internal override async Task<string> CoreSolve(StreamReader reader) 
    {
        var content = await reader.ReadToEndAsync();
        //Fais des trucs batard
        return content;
    }
}
EOF