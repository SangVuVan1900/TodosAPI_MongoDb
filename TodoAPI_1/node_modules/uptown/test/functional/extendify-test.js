var _ = require('lodash');
var expect = require('../test-helper').expect;

var extend = require('../../lib/uptown').extend;
var extendify = require('../../lib/uptown').extendify;

describe('Extendify', function() {
  context('when extendifying a function', function() {
    var Original = function() {};
    extendify(Original);

    it('has the extend method', function() {
      expect(Original.extend).to.equal(extend);
    });
  });
});
