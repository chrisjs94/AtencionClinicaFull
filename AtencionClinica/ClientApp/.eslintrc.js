module.exports = {
    "env": {
        "browser": true,
        "es2021": true
    },
    "extends": "plugin:react/recommended",
    "overrides": [
    ],
    "parser": "@typescript-eslint/parser",
    "parserOptions": {
        "ecmaVersion": "latest",
        "sourceType": "module"
    },
    "plugins": [
        "react",
        "@typescript-eslint"
    ],
    "rules": {
        "import/prefer-default-export": 0,
        "react/prop-types": 0,
        "lit-a11y/click-events-have-key-events": 0,
        "jsx-a11y/no-static-element-interactions": 0
    }
}
