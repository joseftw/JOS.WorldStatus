const webpack = require('webpack');
const path = require('path');

const devConfig = {
  production: false,
  devTool: 'inline-sourcemap',
  outputPath: path.join(__dirname, 'src/dist')
};

const productionConfig = {
  production: true,
  devTool: false,
  outputPath: path.join(__dirname, '../JOS.WorldStatus/wwwroot')
};

module.exports = function(args = {}) {
  const config = args.production ? productionConfig : devConfig;
  const webpackConfig = {
    context: path.join(__dirname, 'src'),
    devtool: config.devTool,
    entry: './js/client.js',
    module: {
      loaders: [
        {
          test: /\.jsx?$/,
          exclude: /(node_modules|bower_components)/,
          loader: 'babel-loader',
          query: {
            presets: ['react', 'es2015', 'stage-0'],
            plugins: ['react-html-attrs', 'transform-decorators-legacy', 'transform-class-properties']
          }
        }
      ]
    },
    output: {
      path: config.outputPath,
      filename: 'client.min.js'
    },
    plugins: !config.production ? [
      new webpack.HotModuleReplacementPlugin()
    ] : [
      new webpack.optimize.OccurrenceOrderPlugin(),
      new webpack.optimize.UglifyJsPlugin({ mangle: false, sourcemap: false })
    ]
  };

  return webpackConfig;
};
