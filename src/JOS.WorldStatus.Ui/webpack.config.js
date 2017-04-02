const webpack = require('webpack');
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');

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
  new webpack.HotModuleReplacementPlugin()
];

const productionPlugins = [
  new webpack.optimize.OccurrenceOrderPlugin(),
  new webpack.optimize.UglifyJsPlugin({
    mangle: false,
    sourcemap: false,
    minify: true
  }),
  new CleanWebpackPlugin([path.join(productionConfig.outputPath, 'static')], {
    root: path.join(__dirname, '..\\'),
    verbose: true,
    dry: false
  })
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
      filename: 'static/client-[hash].min.js'
    },
    plugins: getPlugins(args.production)
  };

  console.log('Using following config: ', config);
  return webpackConfig;
};
