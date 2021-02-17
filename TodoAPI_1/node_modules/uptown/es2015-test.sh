#!/bin/bash

# Conditionally run the ES2015 tests
if [[ `node -v` == v6* ]] ;
then
    node es2015-test.js
fi
