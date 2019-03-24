angular.module('app.controllers')
    .controller('accountSignUpController', ['$scope', '$form', '$auth', ($scope, $form, $auth) => {

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

        $scope.specialties = [
            { id: 1, name: 'Pharmacist' },
            { id: 2, name: 'Other (please specify)' }
        ];
        $scope.pharmacySettings = [
            { id: 1, name: 'Hospital Pharmacy' },
            { id: 2, name: 'Community Pharmacy' },
            { id: 3, name: 'Other (please specify)' }
        ];

        $scope.specialty = $scope.specialties[0];
        $scope.pharmacySetting = $scope.pharmacySettings[0];

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;

        $scope.submit = (form) => {

            $form.submit($scope, form, () => {

                let model = angular.copy($scope.model);

                debugger

                return $auth.signUp(model)
                    .then(
                        (response) => {

                            $scope.status = 200;
                            $scope.status = 200;
                            $scope.description = response.description;
                        },
                        (error) => {

                            $scope.status = error.status;
                            $scope.description = error.data.error_description;
                        })
                    .finally(() => { $scope.isBusy = false });
            });
        };
        $scope.$watch('specialty', newValue => {

            $scope.model.specialtyId = newValue.id;

            if (newValue.id !== 2)
                $scope.model.specialty = null;                
        });
        $scope.$watch('pharmacySetting', newValue => {

            $scope.model.pharmacySettingId = newValue.id;

            if (newValue.id !== 3)
                $scope.model.pharmacySetting = null;
        });

    }]);