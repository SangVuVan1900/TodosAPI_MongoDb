'use strict';

var _interopRequireDefault = require('babel-runtime/helpers/interop-require-default')['default'];

Object.defineProperty(exports, '__esModule', {
  value: true
});
exports.createHeaders = createHeaders;
exports.pushHeader = pushHeader;
exports.pushHeaderObject = pushHeaderObject;

var _link = require('./link');

var _link2 = _interopRequireDefault(_link);

var _annotations = require('./annotations');

var _annotations2 = _interopRequireDefault(_annotations);

function createHeaders(payload, parser) {
  var HttpHeaders = parser.minim.elements.HttpHeaders;

  var headers = new HttpHeaders();

  payload.headers = payload.headers || headers;
}

function pushHeader(key, value, payload, parser, fragment) {
  var MemberElement = parser.minim.elements.Member;

  var header = undefined;

  createHeaders(payload, parser);

  var duplicate = payload.headers.find(function (member) {
    return member.key.content.toLowerCase() === key.toLowerCase();
  });

  if (duplicate.length) {
    header = duplicate.first();
    header.value = value;
  } else {
    header = new MemberElement(key, value);
  }

  if (fragment) {
    _link2['default'].inferred(fragment, header, parser);
  } else {
    header._meta = parser.minim.toElement({});
  }

  if (fragment === undefined && parser.generateSourceMap) {
    parser.createSourceMap(header, parser.path);
  }

  if (!duplicate.length) {
    payload.headers.push(header);
  }

  return header;
}

function pushHeaderObject(key, header, payload, parser) {
  var value = '';

  if (header.type === 'array') {
    parser.createAnnotation(_annotations2['default'].DATA_LOST, parser.path, 'Headers of type array are not yet supported');

    return;
  }

  // Choose the first available option
  if (header['enum']) {
    // TODO: This may lose data if there are multiple enums.
    value = header['enum'][0];
  }

  if (header['x-example']) {
    value = header['x-example'];
  } else if (header['default']) {
    value = header['default'];
  }

  var headerElement = pushHeader(key, value, payload, parser);

  if (header.description) {
    headerElement.description = header.description;

    if (parser.generateSourceMap) {
      parser.createSourceMap(headerElement.meta.get('description'), parser.path.concat(['description']));
    }
  }
}

exports['default'] = { pushHeader: pushHeader, pushHeaderObject: pushHeaderObject };
//# sourceMappingURL=headers.map