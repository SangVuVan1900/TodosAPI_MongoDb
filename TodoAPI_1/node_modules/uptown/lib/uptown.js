var _ = require('lodash');

// This function borrows very heavily from Backbone's extend function
// See https://github.com/jashkenas/backbone/blob/938a8ff934fd4de4f0009f68d43f500f5920b490/backbone.js#L1821-L1852
exports.extend = function(protoProps, staticProps, gettersSetters) {
  var parent = this;
  var child;

  // Allow for specifying a constructor method for extend
  if (protoProps && _.has(protoProps, 'constructor')) {
    child = protoProps.constructor;
  } else {
    child = function() { return parent.apply(this, arguments); };
  }

  // Inherit prototype from parent along with any given methods or properties
  child.prototype = _.create(parent.prototype, protoProps);

  // Allow for defining getters and setters on new classes
  if (gettersSetters && _.isObject(gettersSetters)) {
    _.forEach(gettersSetters, function(value, key) {
      Object.defineProperty(child.prototype, key, value);
    });
  }

  // Add static methods and properties from the parent and from given static
  // properites (if provided);
  _.extend(child, parent, staticProps);

  return child;
}

// Takes a class and adds extend to it
exports.extendify = function(klass) {
  klass.extend = exports.extend;
}

// Create a new class using extend
exports.createClass = function(protoProps, staticProps, gettersSetters) {
  var Base = function() {};
  exports.extendify(Base);
  return Base.extend(protoProps, staticProps, gettersSetters);
}
