var webpack = require('webpack');
var ExtractTextPlugin          = require('extract-text-webpack-plugin');
module.exports = [{
    entry: "./ts/app.ts",
    output: {
        filename: "../RTFreeWeb/wwwroot/js/app.js"
    },
    // Enable sourcemaps for debugging webpack's output.
    devtool: "source-map",
    resolve: {
        // Add '.ts' and '.tsx' as resolvable extensions.
        extensions: ["", ".ts", ".tsx", ".js"]
    },
    module: {
        loaders: [
            // All files with a '.ts' or '.tsx' extension will be handled by 'ts-loader'.
            { test: /\.tsx?$/, loader: "ts-loader" }
        ]
    }
},
    {
        entry: "./scss/app.scss",
        output: {
            filename: "../RTFreeWeb/wwwroot/css/app.css"
        },
        module: {
            loaders: [
                {
                    test: /\.css$/,
                    loader: ExtractTextPlugin.extract("style-loader", "css-loader")
                },
                {
                    test: /\.scss$/,
                    loader: ExtractTextPlugin.extract("style-loader", "css-loader!sass-loader")
                }
            ]
        },
        plugins: [
            new ExtractTextPlugin("../RTFreeWeb/wwwroot/css/app.css")
        ]
    }
];