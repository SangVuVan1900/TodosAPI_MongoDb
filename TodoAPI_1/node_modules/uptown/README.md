# Uptown

[![Build Status](https://travis-ci.org/smizell/uptown.svg?branch=master)](https://travis-ci.org/smizell/uptown)

Simplify prototypical inheritance. This tries to provide some simple constructs for providing functionality you get with ES6 classes, such as:

* Constructor functions
* Static class methods
* Getter and setter methods

## Install

```sh
npm install uptown --save
```

## Usage

Use `extendify` to add the extend method to a class (**note**: it mutates the original class).

The `extend` class method takes three options arguments:

1. Object of instance properites
1. Object of static methods and properties
1. Object of getter and setter mutators

```js
var extendify = require('uptown').extendify;

var Original = function() {};

Original.prototype.hello = function(value) {
  return 'Hello, ' + value;
}

extendify(Original);

var SubClass = Original.extend({
  // Specify a constructor for the new class
  constructor: function() {
    // Call parent constructor
    Original.apply(this, arguments);
  },

  hello: function(value) {
    // Add exclamation point
    return Original.prototype.hello.call(this, value + '!')
  }
}, {
  // Specify static methods and properties
  staticMethod: function() {}
}, {
  // Specify getters and setters for the new class
  foo: {
    get: function() {},
    set: function() {}
  }
});
```

New classes may also be created using the `createClass` function. The `createClass` function works just like `.extend`.

```js
var createClass = require('uptown').createClass;

var Foo = createClass({
  constructor: function() {
    this.foo = 'bar';
  }
});
```
