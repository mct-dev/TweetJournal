module.exports = {
    'env': {
        'browser': true,
        'es6': true,
    },
    'extends': [
        'google',
    ],
    'globals': {
        'Atomics': 'readonly',
        'SharedArrayBuffer': 'readonly',
    },
    'parserOptions': {
        'ecmaVersion': 11,
        'sourceType': 'module',
    },
    'rules': {
        "indent": ["error", 4],
        "require-jsdoc": [1, {
            "require": {
                "FunctionDeclaration": false,
                "MethodDefinition": false,
                "ClassDeclaration": true,
                "ArrowFunctionExpression": false,
                "FunctionExpression": false
            }
        }]
    },
};
