const webpack = require('webpack');
const path = require('path');

module.exports = function(args = {}) {
  const production = args.production;
  const webpackConfig = {
    context: path.join(__dirname, 'src'),
    devtool: !production ? 'inline-sourcemap' : false,
    entry: './js/client.js',
    module: {
      loaders: [
        {
          test: /\.jsx?$/,
          exclude: /(node_modules|bower_components)/,
          loader: 'babel-loader',
          query: {
            presets: ['react', 'es2015', 'stage-0'],
            plugins: ['react-html-attrs', 'transform-decorators-legacy', 'transform-class-properties'],
          }
        }
      ]
    },
    output: {
      path: path.join(__dirname, 'src/dist'),
      filename: 'client.min.js'
    },
    plugins: !production ? [
      new webpack.HotModuleReplacementPlugin()
    ] : [
      new webpack.optimize.OccurrenceOrderPlugin(),
      new webpack.optimize.UglifyJsPlugin({ mangle: false, sourcemap: false })
    ]
  };

  return webpackConfig;
};
