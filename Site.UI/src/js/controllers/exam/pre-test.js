angular.module('app.controllers')
    .controller('examPreTestController', ['$scope', '$form', '$api', '$state', '$dict', ($scope, $form, $api, $state, $dict) => {

        const _exam = 'pre-test';

        let params = $state.params;

        $scope.model = {
            questions: $dict.questions(),
            newsletterLink: `/pdf/newsletter-${params.locale}.pdf`
        };

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;

        $scope.view = 'questioner';
        $scope.isAllSelected = false;

        $scope.onSelect = () => {

            if ($scope.isAllSelected)
                return;

            let questions = $scope.model.questions;
            for (let index = 0; index < questions.length; index++) {

                if (!questions[index].selectedAnswer)
                    return;
            }

            $scope.isAllSelected = true;
        };
        $scope.submit = (form) => {

            $form.submit($scope, form, () => {

                let json = {};
                let correctAnswers = 0;
                let questions = $scope.model.questions;
                for (let index = 0; index < questions.length; index++) {

                    let question = questions[index];
                    let selectedAnswer = question.selectedAnswer;

                    if (selectedAnswer.isCorrect)
                        correctAnswers++;

                    json[`answer${index}`] = question.answers.indexOf(selectedAnswer);
                }

                json.percentCorrect = Math.round((correctAnswers / questions.length) * 100);

                return $api.examPost(_exam, json)
                    .then(
                        (response) => {

                            $state.go('exam');
                        },
                        (error) => {

                            $scope.status = error.status;
                            $scope.description = error.data.error_description;
                        })
                    .finally(() => { $scope.isBusy = false });
            });
        };


    }]);