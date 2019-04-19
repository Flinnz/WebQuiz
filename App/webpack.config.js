'use strict';

const path = require("path");
const webpack = require('webpack');

const bundleFolder = "./src/dist/";
const srcFolder = "./src/"


module.exports = {
    entry: srcFolder + "index.jsx",
    devtool: "source-map",
    output: {
        path: path.resolve(__dirname, bundleFolder),
        publicPath: 'dist/',
        filename: "bundle.js",
    },
    module: {
        rules: [
            {
                test: /\.jsx$/,
                exclude: /(node_modules)/,
                loader: "babel-loader",
            },
            {
                test: /\.css$/,
                use: [{ loader: 'style-loader' }, { loader: 'css-loader' }],
            },
            { 
                test: /\.(png|jpg|jpeg|gif|svg)$/, 
                use: 'url-loader?limit=25000' 
            },
        ]
    },
    devServer: {
        contentBase: path.join(__dirname, srcFolder),
        compress: true,
        port: 9000,
        https: true,
        proxy: {
            '/api': { 
                target: 'https://localhost:5001',
                secure: false,
            },
            '/multiplayer': {
                target: 'https://localhost:5001',
                secure: false,
            },
            '/swagger': {
                target: 'https://localhost:5001',
                secure: false,
            },
            '/multiplayer': {
                target: 'wss://localhost:5001',
                ws: true,
                secure: false,
            },
        },
    },
}