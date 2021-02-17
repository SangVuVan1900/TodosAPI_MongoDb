var _ = require('lodash');
var expect = require('../test-helper').expect;

var createClass = require('../../lib/uptown').createClass;

describe('Creating Classes', function() {
  var Foo = createClass({
    constructor: function() {
      this._x = 1;
    }
  }, {
    staticMethod: function() {
      return 'Hello world';
    }
  }, {
    x: {
      get: function() {
        return this._x;
      },
      set: function(value) {
        this._x = value;
      }
    }
  })

  var foo = new Foo();

  it('creates a class that can be initiated', function() {
    expect(foo).to.be.instanceOf(Foo);
  });

  it('attaches static methods', function() {
    expect(Foo.staticMethod()).to.equal('Hello world');
  });

  it('attaches getters and setters', function() {
    expect(foo.x).to.equal(1);
    foo.x = 2;
    expect(foo.x).to.equal(2);
  })
});
