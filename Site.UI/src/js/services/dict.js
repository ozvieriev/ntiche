angular.module('app.services')
    .factory('$dict', ['$sce', '$filter', ($sce, $filter) => {

        let _questions = (() => {

            return [
                {
                    text: 'Which of the following is not considered a Critical Dose Drug by Health Canada?',
                    answers: [
                        { text: 'Cyclosporine' },
                        { text: 'Mycophenolate Mofetil', isCorrect: true },
                        { text: 'Sirolimus' },
                        { text: 'Tacrolimus' }
                    ]
                },
                {
                    text: 'Which of the following statements is <u>true</u> regarding Critical Dose Drugs?',
                    answers: [
                        { text: 'Critical dose drugs have a relatively large therapeutic index' },
                        { text: 'Critical dose drugs are drugs for which comparatively large differences in dose or concentration can lead to therapeutic failures' },
                        { text: 'Critical dose drugs are drugs for which comparatively small differences in dose or concentration can lead to potentially serious adverse reactions or therapeutic failures', isCorrect: true },
                        { text: 'Critical dose drugs are biologic medical products that are almost an identical copy of the original innovator product' }
                    ]
                },
                {
                    text: 'Which of the following statements from the Canadian Society of Transplantation recommendations on generic immunosuppression in solid organ transplant recipients is <u>false</u>?',
                    answers: [
                        { text: 'Generic immunosuppression use should be approached with caution' },
                        { text: 'Close monitoring is essential when initiating therapy and with any change in critical dose drug product' },
                        { text: 'Patient education regarding generic medications and generic substitution is essential' },
                        { text: 'Generic immunosuppressive formulations in pediatric solid organ transplant recipients is recommended', isCorrect: true }
                    ]
                },
                {
                    text: 'Which of the following statements is <u>true</u> regarding bioequivalence or alternative formulations (generics)?',
                    answers: [
                        { text: 'Bioequivalence needs to be shown from a single generic to the innovator formulation, but not from one generic to another generic', isCorrect: true },
                        { text: 'Bioequivalence needs to be shown from a single generic to the innovator formulation, and also from one generic to another generic' },
                        { text: 'For critical dose drugs, the ratio of area under the curve (AUC) for generic vs. innovator formulations should be within the limits of 80-125% in order to be deemed bioequivalent' },
                        { text: 'Bioequivalence is based on the rate and extent of absorption of a drug into the systemic circulation' }
                    ]
                },
                {
                    text: 'Which of the following statements is <u>false</u> regarding intrapatient variability (IPV) in pharmacokinetics?',
                    answers: [
                        { text: 'Intrapatient variability refers to variability within an individual over time' },
                        { text: 'Intrapatient variability refers to variability from one individual to another individual', isCorrect: true },
                        { text: 'High intrapatient variability with critical dose immunosuppressant drugs may put a patient at risk of over- or under-immunosuppression' },
                        { text: 'High intrapatient variability in immunosuppressant drug exposure is associated with poorer outcomes in solid organ transplant recipients' }
                    ]
                },
                {
                    text: 'Which of the following is <b>not considered a key factor</b> in specific target concentrations for therapeutic drug monitoring for narrow therapeutic index drugs?',
                    answers: [
                        { text: 'Transplanted organ' },
                        { text: 'Risk of rejection (immunologic risk)' },
                        { text: 'Gender', isCorrect: true },
                        { text: 'Time post-transplant' }
                    ]
                },
                {
                    text: 'Which of the following statements regarding prescribability and switchability of alternative formulations is <u>false</u>?',
                    answers: [
                        { text: 'Prescribability refers to the confidence in safety/efficacy of bioequivalence when prescribing a drug to a naïve patient' },
                        { text: 'Switchability refers to the appropriate transfer of a patient from one drug product formulation to another and may require dose adjustment' },
                        { text: 'Switchability refers to the confidence in safety/efficacy of bioequivalence when prescribing a drug to a naïve patient', isCorrect: true },
                        { text: 'Switchability is not currently part of Health Canada requirements for generic interchangeability' }
                    ]
                }
            ];
        });
        let service = {};
        
        let viewQuestion = function (json) {

            let text = json.text;

            text && (text = $filter('translate')(text));

            this.text = $sce.trustAsHtml(text);
            this.answers = viewAnswerBuilder.build(json);
            this.selectedAnswer = null;
        };
        let viewAnswer = function (json) {

            let text = json.text;

            text && (text = $filter('translate')(text));

            this.text = $sce.trustAsHtml(text);
            this.isCorrect = json.isCorrect;
        };

        let viewQuestionBuilder = function () { };
        viewQuestionBuilder.build = (questions) => {

            let result = [];

            if (questions) {

                for (let index = 0; index < questions.length; ++index) {

                    let question = questions[index];
                    result.push(new viewQuestion(question));
                }
            }
            return result;
        };

        let viewAnswerBuilder = function () { };
        viewAnswerBuilder.build = (json) => {

            let answers = [];

            if (json.answers) {

                for (let index = 0; index < json.answers.length; ++index) {

                    let answer = json.answers[index];
                    answers.push(new viewAnswer(answer));
                }
            }

            return answers;
        };

        service.questions = () => {

            return viewQuestionBuilder.build(_questions());
        };

        return service;
    }]);