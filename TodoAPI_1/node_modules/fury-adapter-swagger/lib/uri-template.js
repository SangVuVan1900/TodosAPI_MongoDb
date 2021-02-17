'use strict';

var _interopRequireDefault = require('babel-runtime/helpers/interop-require-default')['default'];

Object.defineProperty(exports, '__esModule', {
  value: true
});
exports['default'] = buildUriTemplate;

var _lodash = require('lodash');

var _lodash2 = _interopRequireDefault(_lodash);

function buildUriTemplate(basePath, href) {
  var pathObjectParameters = arguments.length <= 2 || arguments[2] === undefined ? [] : arguments[2];
  var queryParameters = arguments.length <= 3 || arguments[3] === undefined ? [] : arguments[3];

  if (queryParameters.length > 0 || pathObjectParameters.length > 0) {
    // Path object parameters apply to all nested resources (operations). Only
    // the ones marked as query parameters are relevant though
    var pathObjectParameterNames = pathObjectParameters.filter(function (parameter) {
      return parameter['in'] === 'query';
    }).map(function (parameter) {
      return parameter.name;
    });

    var queryParameterNames = queryParameters.map(function (parameter) {
      return parameter.name;
    });

    // There can be duplicate parameter names, so we need the unique list
    var parameterNames = _lodash2['default'].uniq([].concat(pathObjectParameterNames, queryParameterNames));
    var parameterNamesString = parameterNames.length ? '{?' + parameterNames.join(',') + '}' : '';

    var full = '' + basePath + href + parameterNamesString;

    // Before returning, we replace instances of `-` with `%2d`, but only when
    // they occur inside of a template variable.
    return full.replace(/\{.*?\}/g, function (match) {
      return match.replace('-', '%2d');
    });
  }

  return basePath + href;
}

module.exports = exports['default'];
//# sourceMappingURL=uri-template.map