const webpack = require('webpack');
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const FaviconsWebpackPlugin = require('favicons-webpack-plugin');

const devConfig = {
  apiUrl: 'http://localhost:5521',
  production: false,
  devTool: 'inline-sourcemap',
  outputPath: path.join(__dirname, 'src/dist')
};

const productionConfig = {
  apiUrl: 'https://worldstatus.josefottosson.se',
  production: true,
  devTool: false,
  outputPath: path.join(__dirname, '../JOS.WorldStatus/wwwroot')
};

const commonPlugins = production => [
  new HtmlWebpackPlugin({
    title: 'Worldstatus!',
    minify: false,
    hash: true,
    template: 'index.html'
  }),
  new webpack.DefinePlugin({
    __SITECONFIG__: production ?
      JSON.stringify(productionConfig) :
      JSON.stringify(devConfig)
  })
];

const devPlugins = [
  new webpack.HotModuleReplacementPlugin(),
  new ExtractTextPlugin({
    filename: 'main-[hash].css',
    allChunks: true,
    disable: true
  })
];

const productionPlugins = [
  new webpack.optimize.OccurrenceOrderPlugin(),
  new webpack.optimize.UglifyJsPlugin({
    mangle: false,
    sourcemap: false,
    minify: true,
    comments: false,
    compress: {
      warnings: false,
      drop_console: true
    }
  }),
  new CleanWebpackPlugin([path.join(productionConfig.outputPath, 'static')], {
    root: path.join(__dirname, '..\\'),
    verbose: true,
    dry: false
  }),
  new ExtractTextPlugin({
    filename: 'static/main-[hash].css',
    allChunks: true,
    disable: false
  }),
  new FaviconsWebpackPlugin('./img/logo.jpg')
];

const getPlugins = (production) => {
  if (production) {
    return [
      ...commonPlugins(production),
      ...productionPlugins
    ];
  }

  return [
    ...commonPlugins(production),
    ...devPlugins
  ];
};

module.exports = function(args = {}) {
  const config = args.production ? productionConfig : devConfig;
  const webpackConfig = {
    context: path.join(__dirname, 'src'),
    devtool: config.devTool,
    entry: './js/app.js',
    module: {
      rules: [
        {
          test: /\.jsx?$/,
          exclude: /(node_modules|bower_components)/,
          loader: 'babel-loader'
        },
        {
          test: /\.scss$/,
          use: ExtractTextPlugin.extract({
            fallback: 'style-loader',
            use: [{
              loader: 'css-loader',
              options: {
                minimize: config.production,
                sourceMap: !config.production
              }
            }, {
              loader: 'sass-loader',
              options: {
                sourceMap: !config.production
              }
            }],
            publicPath: '/dist'
          })
        }
      ]
    },
    output: {
      path: config.outputPath,
      filename: 'static/client-[hash].min.js'
    },
    plugins: getPlugins(args.production)
  };

  console.log('Using following config: ', config);
  return webpackConfig;
};
