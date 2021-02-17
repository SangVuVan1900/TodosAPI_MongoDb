'use strict';

var _interopRequireDefault = require('babel-runtime/helpers/interop-require-default')['default'];

Object.defineProperty(exports, '__esModule', {
  value: true
});
exports.detect = detect;
exports.parse = parse;

var _lodash = require('lodash');

var _lodash2 = _interopRequireDefault(_lodash);

var _parser = require('./parser');

var _parser2 = _interopRequireDefault(_parser);

var name = 'swagger';

exports.name = name;
// TODO: Figure out media type for Swagger 2.0
var mediaTypes = ['application/swagger+json', 'application/swagger+yaml'];

exports.mediaTypes = mediaTypes;

function detect(source) {
  return !!(_lodash2['default'].isString(source) ? source.match(/"?swagger"?:\s*["']2\.0["']/g) : source.swagger === '2.0');
}

/*
 * Parse Swagger 2.0 into Refract elements
 */

function parse(options, done) {
  var parser = new _parser2['default'](options);
  parser.parse(done);
}

exports['default'] = { name: name, mediaTypes: mediaTypes, detect: detect, parse: parse };
//# sourceMappingURL=adapter.map