const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const bundleOutputDir = './wwwroot/dist';

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);

    return [{
        stats: { modules: false },
        context: __dirname,
        resolve: {
            extensions: ['.vue', '.js', '.ts'],
            // prevent a JavaScript console warning
            alias: {
                'vue$': 'vue/dist/vue.esm.js'
            }
        },
        entry: { 'main': './ClientApp/boot.ts' },
        module: {
            rules: [
                {
                    test: /\.vue(\.html)?$/,
                    include: /ClientApp/,
                    loader: 'vue-loader',
                    options: {
                        loaders: {
                            js: 'awesome-typescript-loader?silent=true'
                        }
                    }
                },
                {
                    test: /\.ts$/,
                    include: /ClientApp/,
                    use: 'awesome-typescript-loader?silent=true'
                },
                {
                    test: /\.css$/,
                    use: isDevBuild
                        ? ['style-loader', 'css-loader']
                        : ExtractTextPlugin.extract({ use: 'css-loader?minimize' })
                },
                {
                    test: /\.scss$/,
                    use: isDevBuild
                        ? ['style-loader', 'sass-loader']
                        : ExtractTextPlugin.extract({ use: 'sass-loader?minimize' })
                },
                {
                    test: /\.(png|jpe?g|gif|svg)(\?.*)?$/,
                    loader: 'url-loader',
                    options: {
                        limit: 25000,
                        //name: utils.assetsPath('img/[name].[hash:7].[ext]')
                    }
                },
                {
                    test: /\.(mp4|webm|ogg|mp3|wav|flac|aac)(\?.*)?$/,
                    loader: 'url-loader',
                    options: {
                        limit: 25000,
                        //name: utils.assetsPath('media/[name].[hash:7].[ext]')
                    }
                },
                {
                    test: /\.(woff2?|eot|ttf|otf)(\?.*)?$/,
                    loader: 'url-loader',
                    options: {
                        limit: 25000,
                        //name: utils.assetsPath('fonts/[name].[hash:7].[ext]')
                    }
                }
            ]
        },
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: isDevBuild ? 'dist/' : ''
        },
        plugins: [
            new CheckerPlugin(),
            new webpack.DefinePlugin({
                'process.env': {
                    NODE_ENV: JSON.stringify(isDevBuild ? 'development' : 'production')
                }
            }),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            })
        ].concat(isDevBuild ? [
            // Plugins that apply in development builds only
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map', // Remove this line if you prefer inline source maps
                moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
            })
        ] : [
                // Plugins that apply in production builds only
                new webpack.optimize.UglifyJsPlugin(),
                new ExtractTextPlugin('site.css')
            ])
    }];
};
