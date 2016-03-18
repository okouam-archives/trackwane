import 'angular';
import 'angular-mocks';

const testsContext = require.context('../../src', true, /.tests$/);
testsContext.keys().forEach(testsContext);
