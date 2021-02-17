'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});
exports.baseLink = baseLink;
exports.origin = origin;
exports.inferred = inferred;

function baseLink(element, parser) {
  var options = arguments.length <= 2 || arguments[2] === undefined ? {} : arguments[2];
  var Link = parser.minim.elements.Link;

  var link = new Link();
  link.relation = options.relation || 'origin';
  link.href = 'http://docs.apiary.io/validations/swagger#' + (options.fragment || '');

  element.links.push(link);
}

function origin(fragment, element, parser) {
  baseLink(element, parser, {
    fragment: fragment
  });
}

function inferred(fragment, element, parser) {
  baseLink(element, parser, {
    relation: 'inferred',
    fragment: fragment
  });
}

exports['default'] = { origin: origin, inferred: inferred };
//# sourceMappingURL=link.map