angular.module('app.controllers')
    .controller('examPostTestController', ['$scope', '$form', '$api', '$dict', '$state',
        ($scope, $form, $api, $dict, $state) => {

            const _exam = 'post-test';

            let params = $state.params;

            $scope.model = {
                questions: $dict.questions()
            };

            $scope.status = null;
            $scope.description = null;
            $scope.isBusy = false;

            $scope.view = null;
            $scope.newsletterLink = null;
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
            $scope.answerCssClass = ((question, answer) => {

                if (!answer.isCorrect && question.selectedAnswer == answer)
                    return 'alert-danger';

                if (answer.isCorrect && question.selectedAnswer == answer)
                    return 'alert-success';

                if (!question.selectedAnswer.isCorrect && answer.isCorrect)
                    return 'alert-success';
            });

            $scope.totalFeedbacks = 0;
            $scope.examResultId = null;

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

                                $scope.status = 200;
                                $scope.description = response.description;

                                if (!response.isSuccess) {

                                    $scope.view = 'fail';

                                    if (response.totalFailedPostTests >= 2)
                                        $scope.model.newsletterLink = `/pdf/newsletter-${params.locale}.pdf`;

                                    return;
                                }

                                $scope.totalFeedbacks = response.totalFeedbacks;
                                $scope.examResultId = response.examResultId;
                                $scope.view = 'success';
                            },
                            (error) => {

                                $scope.status = error.status;
                                $scope.description = error.data.error_description;
                            })
                        .finally(() => { $scope.isBusy = false });
                });
            };
            $scope.next = () => {

                if ($scope.totalFeedbacks)
                    return $state.go(`exam/post-test-success`, { examResultId: $scope.examResultId });

                return $state.go(`exam/feedback`, { examResultId: $scope.examResultId });
            };
        }]);