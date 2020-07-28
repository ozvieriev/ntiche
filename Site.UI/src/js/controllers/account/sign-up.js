angular.module('app.controllers')
    .controller('accountSignUpController', ['$scope', '$form', '$auth', '$translate', ($scope, $form, $auth, $translate) => {

        $scope.model = {
            //userName: 'MegaZver',
            //password: 'anatole64',
            //firstName: 'Oleksandr',
            //lastName: 'Zvieriev',
            //email: 'ozvieriev@gmail.com',
            //specialty: 'Web Developer',
            //pharmacistLicense: '87538947593',
            //city: 'Montreal',
            //isOptin: true
        };
        $scope.pharmacySettings = [
            { id: 1, name: 'Hospital Pharmacy' },
            { id: 2, name: 'Community Pharmacy' },
            { id: 3, name: 'Other (please specify)' }
        ];

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;

        $scope.submit = (form) => {

            $form.submit($scope, form, () => {

                let model = angular.copy($scope.model);

                return $auth.signUp(model)
                    .then(
                        (response) => {

                            $scope.status = 200;
                            $scope.status = 200;

                            $translate(response.description)
                                .then(
                                    (description) => { $scope.description = description; },
                                    (description) => { $scope.description = description; });
                        },
                        (error) => {

                            $scope.status = error.status;
                            $translate(error.data.error_description)
                                .then(
                                    (errorDescription) => { $scope.description = errorDescription; },
                                    (errorDescription) => { $scope.description = errorDescription; });
                        })
                    .finally(() => { $scope.isBusy = false });
            });
        };
        $scope.$watch('model.specialtyId', newValue => {

            if (newValue !== '2')
                $scope.model.specialty = null;
        });
        $scope.$watch('model.pharmacySettingId', newValue => {

            if (newValue !== '3')
                $scope.model.pharmacySetting = null;
        });

    }]);